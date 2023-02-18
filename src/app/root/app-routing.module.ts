import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from "./auth.guard";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {AuthRedirectionModel} from "../shared/models/auth-redirection.model";

export const authRedirections = [
  {
    Role: 'user',
    Authenticated: '/restaurants',
    Unauthenticated: '/login',
  },
  {
    Role: 'restaurant_owner',
    Authenticated: '/dashboard',
    Unauthenticated: '/login',
  },
  {
    Role: 'admin',
    Authenticated: '/users',
    Unauthenticated: '/login',
  },
] as AuthRedirectionModel[];

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'restaurants',
    canActivate: [ AuthGuard ],
    loadChildren: () => import('../restaurants/restaurants.module').then(m => m.RestaurantsModule),
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin']
    }
  },
  {
    path: 'reviews',
    canActivate: [ AuthGuard ],
    loadChildren: () => import('../reviews/reviews.module').then(m => m.ReviewsModule),
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin']
    }
  },
  {
    path: 'users',
    canActivate: [ AuthGuard ],
    loadChildren: () => import('../users/users.module').then(m => m.UsersModule),
    data: {
      authRedirections: authRedirections,
      roles: ['admin']
    }
  },
  {
    path: 'dashboard',
    canActivate: [ AuthGuard ],
    loadChildren: () => import('../owner-dashboard/owner-dashboard.module').then(m => m.OwnerDashboardModule),
    data: {
      authRedirections: authRedirections,
      roles: ['restaurant_owner']
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin', 'restaurant_owner']
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin', 'restaurant_owner']
    }
  },
  {
    path: '404',
    canActivate: [ AuthGuard ],
    component: NotFoundComponent,
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin', 'restaurant_owner']
    }
  },
  {
    path: '**',
    redirectTo: '404',
    data: {
      authRedirections: authRedirections,
      roles: ['user', 'admin', 'restaurant_owner']
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
