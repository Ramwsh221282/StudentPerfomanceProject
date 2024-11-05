import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeachersAssignmentPageComponent } from '../components/teachers-assignment-page/teachers-assignment-page.component';

const routes: Routes = [
  { path: '', component: TeachersAssignmentPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeachersRoutingModule {}
