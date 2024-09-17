import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SemesterPageComponent } from './components/semester-page/semester-page.component';

const routes: Routes = [{ path: '', component: SemesterPageComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SemestersRoutingModule {}
