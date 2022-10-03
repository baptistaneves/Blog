import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteRoutingModule } from './site-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    LayoutComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    SiteRoutingModule
  ],
  exports: [
    LayoutComponent
  ]
})
export class SiteModule { }
