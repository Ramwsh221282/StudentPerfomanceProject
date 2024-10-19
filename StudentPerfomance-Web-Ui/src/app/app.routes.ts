import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./shared/components/login-page/login-page.component').then(
        (mod) => mod.LoginPageComponent
      ),
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./shared/components/user-page/user-page.component').then(
        (mod) => mod.UserPageComponent
      ),
  },
  {
    path: 'administration',
    loadChildren: () =>
      import('./modules/administration/administration.module').then(
        (mod) => mod.AdministrationModule
      ),
  },
  {
    path: 'education-directions',
    loadChildren: () =>
      import(
        './modules/administration/submodules/education-directions/education-directions.module'
      ).then((mod) => mod.EducationDirectionsModule),
  },
  {
    path: 'education-plans',
    loadChildren: () =>
      import(
        './modules/administration/submodules/education-plans/education-plans.module'
      ).then((mod) => mod.EducationPlansModule),
  },
  {
    path: 'groups',
    loadChildren: () =>
      import(
        './modules/administration/submodules/student-groups/student-groups.module'
      ).then((mod) => mod.StudentGroupsModule),
  },
  {
    path: 'departments',
    loadChildren: () =>
      import(
        './modules/administration/submodules/departments/departments.module'
      ).then((mod) => mod.DepartmentsModule),
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./modules/administration/submodules/users/users.module').then(
        (mod) => mod.UsersModule
      ),
  },
];
