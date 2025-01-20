import { Routes } from '@angular/router';
import { canActivateAuthAdmin } from './modules/users/services/admin-access-guard';
import { CanActivateAuthTeacher } from './modules/teachers/teachers/teachers-access-guard';
import { CanActivateUserPage } from './shared/components/user-page/user-page-guard-access';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./shared/components/greeting-page/greeting-page.component').then(
        (component) => component.GreetingPageComponent,
      ),
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./shared/components/login-page/login-page.component').then(
        (mod) => mod.LoginPageComponent,
      ),
  },
  {
    path: 'reports',
    loadComponent: () =>
      import(
        './shared/components/session-reports-view/session-reports-view.component'
      ).then((mod) => mod.SessionReportsViewComponent),
    canActivate: [CanActivateUserPage],
  },
  {
    path: 'reset-password',
    loadComponent: () =>
      import(
        './shared/components/login-page/login-form/recovery-confirmation/recovery-confirmation.component'
      ).then((mod) => mod.RecoveryConfirmationComponent),
  },
  {
    path: 'group-reports/:reportId:startDate:closeDate:season:number',
    loadComponent: () =>
      import(
        './shared/components/session-reports-view/session-reports-layout/session-report-item/session-group-reports/session-group-reports.component'
      ).then((mod) => mod.SessionGroupReportsComponent),
    canActivate: [CanActivateUserPage],
  },
  {
    path: 'course-reports/:reportId:startDate:closeDate:season:number',
    loadComponent: () =>
      import(
        './shared/components/session-reports-view/session-reports-layout/session-report-item/session-course-report/session-course-report.component'
      ).then((mod) => mod.SessionCourseReportComponent),
    canActivate: [CanActivateUserPage],
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./shared/components/user-page/user-page.component').then(
        (mod) => mod.UserPageComponent,
      ),
    canActivate: [CanActivateUserPage],
  },
  {
    path: 'teacher-assignments',
    loadChildren: () =>
      import('./modules/teachers/teachers/teachers.module').then(
        (mod) => mod.TeachersModule,
      ),
    canActivate: [CanActivateAuthTeacher],
  },
  {
    path: 'administration',
    loadChildren: () =>
      import('./modules/administration/administration.module').then(
        (mod) => mod.AdministrationModule,
      ),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'administration-instructions',
    loadComponent: () =>
      import(
        './modules/administration/submodules/instructions-module/instructions-page/instructions-page.component'
      ).then((mod) => mod.InstructionsPageComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'education-directions',
    loadChildren: () =>
      import(
        './modules/administration/submodules/education-directions/education-directions.module'
      ).then((mod) => mod.EducationDirectionsModule),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'education-directions-info',
    loadComponent: () =>
      import(
        './modules/administration/components/admin-page-informations/education-directions-info/education-directions-info.component'
      ).then((mod) => mod.EducationDirectionsInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'education-plans',
    loadChildren: () =>
      import(
        './modules/administration/submodules/education-plans/education-plans.module'
      ).then((mod) => mod.EducationPlansModule),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'education-plans-info',
    loadComponent: () =>
      import(
        './modules/administration/submodules/education-plans/education-plans-info/education-plans-info.component'
      ).then((mod) => mod.EducationPlansInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'assignment-sessions',
    loadChildren: () =>
      import(
        './modules/administration/submodules/assignment-sessions/assignment-sessions.module'
      ).then((mod) => mod.AssignmentSessionsModule),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'assignment-sessions-info',
    loadComponent: () =>
      import(
        './modules/administration/submodules/assignment-sessions/components/control-week-info/control-week-info.component'
      ).then((mod) => mod.ControlWeekInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'groups',
    loadChildren: () =>
      import(
        './modules/administration/submodules/student-groups/student-groups.module'
      ).then((mod) => mod.StudentGroupsModule),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'groups-info',
    loadComponent: () =>
      import(
        './modules/administration/submodules/student-groups/components/student-groups-info/student-groups-info.component'
      ).then((mod) => mod.StudentGroupsInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'departments',
    loadChildren: () =>
      import(
        './modules/administration/submodules/departments/departments.module'
      ).then((mod) => mod.DepartmentsModule),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'departments-info',
    loadComponent: () =>
      import(
        './modules/administration/submodules/departments/components/departments-info/departments-info.component'
      ).then((mod) => mod.DepartmentsInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./modules/administration/submodules/users/users.module').then(
        (mod) => mod.UsersModule,
      ),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'users-info',
    loadComponent: () =>
      import(
        './modules/administration/submodules/users/components/users-info/users-info.component'
      ).then((mod) => mod.UsersInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
];
