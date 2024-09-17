import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdministrationPageComponent } from './components/administration-page/administration-page.component';

const routes: Routes = [{ path: '', component: AdministrationPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdministrationRoutingModule {}
