import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EducationPlansPageComponent } from './components/education-plans-page/education-plans-page.component';

const routes: Routes = [{ path: '', component: EducationPlansPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EducationPlansRoutingModule {}
