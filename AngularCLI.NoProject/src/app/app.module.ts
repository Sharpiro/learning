import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { InitCapsPipe } from './shared/init-caps.pipe';
import { SalesDataService } from './sales-data.service';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    InitCapsPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [SalesDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
