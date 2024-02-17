import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './Components/register/register.component';
import { SigninComponent } from './Components/signin/signin.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './Core/requests.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    SigninComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideClientHydration(),
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true } // Provide the interceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
