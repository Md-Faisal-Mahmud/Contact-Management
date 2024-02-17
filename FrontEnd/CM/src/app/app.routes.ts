import { Routes } from '@angular/router';
import { RegistrationComponent } from './Component/registration/registration.component';

export const routes: Routes = [
    { path: '', redirectTo: '/register', pathMatch: 'full' }, 
  { path: 'register', component: RegistrationComponent } 
];
