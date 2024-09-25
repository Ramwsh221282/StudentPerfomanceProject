import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EducationDirectionsPageComponent } from './components/education-directions-page/education-directions-page.component';

const routes: Routes = [
  { path: '', component: EducationDirectionsPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EducationDirectionsRoutingModule {}
