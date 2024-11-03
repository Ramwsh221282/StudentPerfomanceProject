import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssignmentSessionsPageComponent } from './components/assignment-sessions-page/assignment-sessions-page.component';

const routes: Routes = [
  { path: '', component: AssignmentSessionsPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AssignmentSessionsRoutingModule {}
