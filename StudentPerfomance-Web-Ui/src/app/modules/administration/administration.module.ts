import { NgModule } from '@angular/core';
import { AdministrationPageComponent } from './components/administration-page/administration-page.component';
import { AdministrationMenuComponent } from './components/administration-menu/administration-menu.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { NgForOf, NgIf } from '@angular/common';
import { BlueButtonComponent } from '../../building-blocks/buttons/blue-button/blue-button.component';

@NgModule({
  declarations: [AdministrationPageComponent, AdministrationMenuComponent],
  imports: [AdministrationRoutingModule, NgIf, BlueButtonComponent, NgForOf],
  exports: [AdministrationPageComponent],
})
export class AdministrationModule {}
