import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentPageComponent } from './components/student-page/student-page.component';

const routes: Routes = [{ path: '', component: StudentPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentsRoutingModule {}
