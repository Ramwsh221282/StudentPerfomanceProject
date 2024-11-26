import { NgModule } from '@angular/core';
import { AdministrationPageComponent } from './components/administration-page/administration-page.component';
import { AdministrationMenuComponent } from './components/administration-menu/administration-menu.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { NgIf } from '@angular/common';

@NgModule({
  declarations: [AdministrationPageComponent, AdministrationMenuComponent],
  imports: [AdministrationRoutingModule, NgIf],
  exports: [AdministrationPageComponent],
})
export class AdministrationModule {}
