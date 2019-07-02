import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HttpClientModule } from '@angular/common/http';
import { DashComponent } from './dash/dash.component';
import { OwnerComponent } from './Admin/owner/owner.component';
import { OwnerDashComponent } from './ownerDash/ownerDash.component';
import { MaterialModule } from './material.module';
import { Pipe1Component } from './pipe/pipe1/pipe1.component';
import { Pipe2Component } from './pipe/pipe2/pipe2.component';
import { Pipe3Component } from './pipe/pipe3/pipe3.component';
import { ViewOwnersComponent } from './AdminControls/viewOwners/viewOwners.component';
import { NavComponent } from './Frags/nav/nav.component';
import { FooterComponent } from './Frags/footer/footer.component';
import { TermsComponent } from './terms/terms.component';
import { VerificationComponent } from './verification/verification.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';


@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      RegisterComponent,
      DashComponent,
      OwnerComponent,
      OwnerDashComponent,
      TermsComponent,
      //pipes\\\\\\\\nPipe1Component,
      Pipe1Component,
      Pipe2Component,
      Pipe3Component,
      //admincontrols\\\\\\\\nViewOwnersComponent,
      NavComponent,
      FooterComponent,
      ViewOwnersComponent,
      VerificationComponent,
      ResetPasswordComponent,
      ForgotPasswordComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      MaterialModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
