import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentGroupsPageComponent } from './components/student-groups-page/student-groups-page.component';

const routes: Routes = [{ path: '', component: StudentGroupsPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentGroupsRoutingModule {}
