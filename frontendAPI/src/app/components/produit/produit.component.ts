import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductModel } from '../../models/ProductModel';
import { ProductService } from '../../services/ProductService';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/AuthService';
import { Subscription } from 'rxjs';
import { EventService } from '../../services/EventService';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

// Importer les dialogs
import { AddEditProductDialog } from './add-edit-product-dialog/add-edit-product-dialog.component';
import { DeleteProductDialog } from './delete-product-dialog/delete-product-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-produit',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatDividerModule,
    MatPaginatorModule
  ],
  templateUrl: './produit.component.html',
  styleUrls: ['./produit.component.css']
})
export class ProduitComponent implements OnInit, OnDestroy {

  products: ProductModel[] = [];
  filteredProducts: ProductModel[] = []; // ← Ajouter pour la recherche
  paginatedProducts: ProductModel[] = [];
  errorMessage: string = '';

  // Pagination
  currentPage: number = 1;
  pageSize: number = 5;

  user: any = null;

  // 🔔 Subscriptions aux events
  private subscriptions: Subscription[] = [];

  // Table columns
  displayedColumns: string[] = ['name', 'price', 'description', 'actions'];

  // Variable pour la recherche
  searchTerm: string = '';

  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private eventService: EventService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    if (!this.authService.isLoggedIn()) {
      this.authService.logout();
      return;
    }
    this.user = this.authService.getUser();

    this.loadProducts();

    // 👉 démarrer la connexion SignalR
    this.eventService.startConnection();

    // 👉 écouter les events produits
    this.subscriptions.push(
      this.eventService.productCreated$.subscribe(event => {
        if (event) {
          console.log('📢 Produit créé:', event);
          this.loadProducts();
        }
      }),
      this.eventService.productUpdated$.subscribe(event => {
        if (event) {
          console.log('✏️ Produit mis à jour:', event);
          this.loadProducts();
        }
      }),
      this.eventService.productDeleted$.subscribe(event => {
        if (event) {
          console.log('🗑️ Produit supprimé:', event);
          this.loadProducts();
        }
      })
    );
  }

  ngOnDestroy(): void {
    // 👉 éviter fuites mémoire
    this.subscriptions.forEach(sub => sub.unsubscribe());
    this.eventService.stopConnection();
  }

  loadProducts(): void {
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.filteredProducts = data; // ← Initialiser filteredProducts
        this.applyFilter(); // ← Appliquer le filtre après chargement
      },
      error: (err) => this.errorMessage = 'Erreur lors du chargement des produits: ' + err
    });
  }

  // MÉTHODE DE RECHERCHE ← AJOUTÉE
  applyFilter(): void {
    if (!this.searchTerm.trim()) {
      this.filteredProducts = this.products;
    } else {
      const searchLower = this.searchTerm.toLowerCase().trim();
      this.filteredProducts = this.products.filter(product =>
        product.name.toLowerCase().includes(searchLower) ||
        product.description.toLowerCase().includes(searchLower) ||
        product.price.toString().includes(searchLower)
      );
    }
    
    this.currentPage = 1; // ← Revenir à la première page après filtrage
    this.updatePaginatedProducts();
  }

  // MÉTHODE POUR RÉINITIALISER LA RECHERCHE ← AJOUTÉE
  clearSearch(): void {
    this.searchTerm = '';
    this.applyFilter();
  }

  updatePaginatedProducts(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedProducts = this.filteredProducts.slice(startIndex, endIndex); // ← Utiliser filteredProducts
  }

  totalPages(): number {
    return Math.ceil(this.filteredProducts.length / this.pageSize); // ← Utiliser filteredProducts
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages()) {
      this.currentPage++;
      this.updatePaginatedProducts();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedProducts();
    }
  }

  // NOUVELLES MÉTHODES POUR LES DIALOGS

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddEditProductDialog, {
      width: '500px',
      data: {
        product: { id: 0, name: '', price: 0, description: '' },
        isEditing: false
      }
    });

    dialogRef.afterClosed().subscribe((result: ProductModel) => {
      if (result) {
        this.productService.create(result).subscribe({
          next: () => this.loadProducts(),
          error: (err) => this.errorMessage = 'Erreur création: ' + err
        });
      }
    });
  }

  openEditDialog(product: ProductModel): void {
    const dialogRef = this.dialog.open(AddEditProductDialog, {
      width: '500px',
      data: {
        product: { ...product },
        isEditing: true
      }
    });

    dialogRef.afterClosed().subscribe((result: ProductModel) => {
      if (result && result.id) {
        this.productService.update(result.id, result).subscribe({
          next: () => this.loadProducts(),
          error: (err) => this.errorMessage = 'Erreur modification: ' + err
        });
      }
    });
  }

  openDeleteDialog(product: ProductModel): void {
    const dialogRef = this.dialog.open(DeleteProductDialog, {
      width: '400px',
      data: { productName: product.name }
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result && product.id) {
        this.productService.delete(product.id).subscribe({
          next: () => this.loadProducts(),
          error: (err) => this.errorMessage = 'Erreur suppression: ' + err
        });
      }
    });
  }

  onPageChange(event: any): void {
  this.currentPage = event.pageIndex + 1;
  this.pageSize = event.pageSize;
  this.updatePaginatedProducts();
}

  logout(): void {
    if (confirm('Voulez-vous vraiment vous déconnecter ?')) {
      this.authService.logout();
    }
  }

}