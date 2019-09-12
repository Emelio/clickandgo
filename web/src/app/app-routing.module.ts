import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DashComponent } from './dash/dash.component';
import { OwnerComponent } from './Admin/owner/owner.component';
import { OwnerDashComponent } from './ownerDash/ownerDash.component';
import { Pipe1Component } from './pipe/pipe1/pipe1.component';
import { Pipe2Component } from './pipe/pipe2/pipe2.component';
import { Pipe3Component } from './pipe/pipe3/pipe3.component';
import { ViewOwnersComponent } from './AdminControls/viewOwners/viewOwners.component';
import { TermsComponent } from './terms/terms.component';
import { VerificationComponent } from './verification/verification.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { RegisterAdminComponent } from './Admin/register-admin/register-admin.component';

const routes: Routes = [
  {
    path: 'verification/:code/:email',
    component: VerificationComponent
  },
  {
    path: 'forgotpassword/:code/:email',
    component: ForgotPasswordComponent
  },
  {
    path: 'pipe',
    children: [
      {
        path: 'pipe1',
    component: Pipe1Component,
      },
      {
        path: 'pipe2',
        component: Pipe2Component
      },
      {
        path: 'pipe3',
        component: Pipe3Component
      }
    ]
  },
  {
    path: 'admin', 
    children: [
      {
        path: 'viewOwners',
        component: ViewOwnersComponent
      }
    ]
  },
  {
    path: 'terms',
    component: TermsComponent
  },
  
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'manageAdmin',
    component: RegisterAdminComponent
  },
  {
    path: 'owner',
    component: OwnerComponent
  },
  {
    path: 'ownerDash',
    component: OwnerDashComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'resetpassword',
    component: ResetPasswordComponent
  },
  {
    path: 'dash',
    component: DashComponent
  },
  {
    path: '',
    component: LoginComponent
  },
  {
    path: '**',
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})

export class AppRoutingModule { }
