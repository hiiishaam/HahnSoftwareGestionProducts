import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ProductModel } from '../../../models/ProductModel';

@Component({
  selector: 'app-add-edit-product-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule
  ],
  templateUrl: './add-edit-product-dialog.component.html'
})
export class AddEditProductDialog {
  isEditing: boolean = false;
  errorMessage: string = '';

  constructor(
    public dialogRef: MatDialogRef<AddEditProductDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { 
      product: ProductModel; 
      isEditing: boolean 
    }
  ) {
    this.isEditing = data.isEditing;
  }

 save(): void {
    // VALIDATION DANS LE DIALOG ← CORRECTION
    if (!this.data.product.name || !this.data.product.description || this.data.product.price <= 0) {
      this.errorMessage = 'Veuillez remplir tous les champs correctement.';
      return; // ← NE PAS FERMER LE DIALOG
    }
    
    this.dialogRef.close(this.data.product);
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}