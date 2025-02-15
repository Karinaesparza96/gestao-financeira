import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const authGuard = () => {
  const router = inject(Router);

  const userData = localStorage.getItem('user');
  if (userData) {
    return true;
  }

  router.navigate(['/conta/login']);
  return false;
};
