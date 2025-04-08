import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private router: Router) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    
    // Check if user is authenticated (replace with your actual auth check)
    const isAuthenticated = localStorage.getItem('token') !== null; 
    
    if (isAuthenticated) {
      return true;
    } else {
      // Redirect to login page if not authenticated
      this.router.navigate(['/Login']);
      return false;
    }
  }
}
