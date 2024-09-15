import { Routes } from '@angular/router';
import { DbmanagerMenuComponent } from './components/dbmanager-menu/dbmanager-menu.component';
import { DbmanagerStudentgroupsViewComponent } from './components/dbmanager-menu/dbmanager-studentgroups-view/dbmanager-studentgroups-view.component';
import { DbmanagerDepartmentsViewComponent } from './components/dbmanager-menu/dbmanager-departments-view/dbmanager-departments-view.component';
import { GroupMenuComponent } from './components/dbmanager-menu/dbmanager-studentgroups-view/group-menu/group-menu.component';
import { DepartmentMenuComponent } from './components/dbmanager-menu/dbmanager-departments-view/department-menu/department-menu.component';
import { DbmanagerSemestersViewComponent } from './components/dbmanager-menu/dbmanager-semesters-view/dbmanager-semesters-view.component';
import { DbmanagerSemesterMenuComponent } from './components/dbmanager-menu/dbmanager-semesters-view/dbmanager-semester-menu/dbmanager-semester-menu.component';

export const routes: Routes = [
  {
    path: 'database-manager-menu',
    component: DbmanagerMenuComponent,
  },
  {
    path: 'database-manager-studentgroups',
    component: DbmanagerStudentgroupsViewComponent,
  },
  {
    path: 'studentgroup-menu/:groupName',
    component: GroupMenuComponent,
  },
  {
    path: 'database-manager-departments',
    component: DbmanagerDepartmentsViewComponent,
  },
  {
    path: 'department-menu/:departmentName',
    component: DepartmentMenuComponent,
  },
  {
    path: 'database-manager-semesters',
    component: DbmanagerSemestersViewComponent,
  },
  {
    path: 'semester-menu/:groupName/:number/:contractsCount',
    component: DbmanagerSemesterMenuComponent,
  },
];
