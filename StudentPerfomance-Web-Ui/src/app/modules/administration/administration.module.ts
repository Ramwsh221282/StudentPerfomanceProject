import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrationPageComponent } from './components/administration-page/administration-page.component';
import { AdministrationMenuComponent } from './components/administration-menu/administration-menu.component';
import { FormsModule } from '@angular/forms';
import { AdministrationRoutingModule } from './administration-routing.module';

@NgModule({
  declarations: [AdministrationPageComponent, AdministrationMenuComponent],
  imports: [CommonModule, AdministrationRoutingModule, FormsModule],
  exports: [AdministrationPageComponent],
})
export class AdministrationModule {}
