import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { AuthService } from '../../../services/AuthService';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    RouterModule,
    MatSelectModule,
    MatOptionModule
  ],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  userData = {
    fullName: '',
    email: '',
    password: '',
    role: 'user'
  };
  
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  register(): void {
    // Réinitialiser les messages
    this.errorMessage = '';
    this.successMessage = '';

    this.loading = true;

    // Appel au service d'inscription
    this.authService.register(this.userData).subscribe({
      next: (response) => {
        this.loading = false;
        this.successMessage = 'Inscription réussie !';
        
        // Redirection après 2 secondes
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (error) => {
        this.loading = false;
        this.errorMessage = 'Erreur lors de l\'inscription';
      }
    });
  }
}