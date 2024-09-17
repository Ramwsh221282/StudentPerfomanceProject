import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlanPageComponent } from './components/plan-page/plan-page.component';

const routes: Routes = [{ path: '', component: PlanPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SemesterPlansRoutingModule {}
