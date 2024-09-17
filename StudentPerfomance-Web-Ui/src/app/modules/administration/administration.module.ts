import { NgModule } from '@angular/core';
import { AdministrationPageComponent } from './components/administration-page/administration-page.component';
import { AdministrationMenuComponent } from './components/administration-menu/administration-menu.component';
import { AdministrationRoutingModule } from './administration-routing.module';

@NgModule({
  declarations: [AdministrationPageComponent, AdministrationMenuComponent],
  imports: [AdministrationRoutingModule],
  exports: [AdministrationPageComponent],
})
export class AdministrationModule {}
