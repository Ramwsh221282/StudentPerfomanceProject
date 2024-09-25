import { Routes } from '@angular/router';

export const routes: Routes = [
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
    path: 'groups',
    loadChildren: () =>
      import(
        './modules/administration/submodules/student-groups/student-groups.module'
      ).then((mod) => mod.StudentGroupsModule),
  },
  {
    path: 'students/:groupName',
    loadChildren: () =>
      import(
        './modules/administration/submodules/students/students.module'
      ).then((mod) => mod.StudentsModule),
  },
  {
    path: 'departments',
    loadChildren: () =>
      import(
        './modules/administration/submodules/departments/departments.module'
      ).then((mod) => mod.DepartmentsModule),
  },
  {
    path: 'teachers/:departmentName',
    loadChildren: () =>
      import(
        './modules/administration/submodules/teachers/teachers.module'
      ).then((mod) => mod.TeachersModule),
  },
  {
    path: 'semesters',
    loadChildren: () =>
      import(
        './modules/administration/submodules/semesters/semesters.module'
      ).then((mod) => mod.SemestersModule),
  },
  {
    path: 'plans/:groupName/:number/:contractsCount',
    loadChildren: () =>
      import(
        './modules/administration/submodules/semester-plans/semester-plans.module'
      ).then((mod) => mod.SemesterPlansModule),
  },
];
