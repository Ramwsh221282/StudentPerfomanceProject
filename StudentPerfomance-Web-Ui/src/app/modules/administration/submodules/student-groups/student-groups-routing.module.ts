import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageGroupComponent } from './components/page-group/page-group.component';

const routes: Routes = [{ path: '', component: PageGroupComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentGroupsRoutingModule {}
