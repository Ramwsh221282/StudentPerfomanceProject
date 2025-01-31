import { Routes } from '@angular/router';
import { canActivateAuthAdmin } from './pages/user-page/services/admin-access-guard';
import { CanActivateUserPage } from './pages/user-page/user-page-guard-access';
import { CanActivateAuthTeacher } from './pages/assignments-page/teachers/teachers-access-guard';

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
    path: 'users',
    loadComponent: () =>
      import(
        './pages/users-management-page/users-management-page.component'
      ).then((component) => component.UsersManagementPageComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'education-objects',
    loadComponent: () =>
      import(
        './pages/education-objects-page/education-objects-page.component'
      ).then((component) => component.EducationObjectsPageComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'teachers',
    loadComponent: () =>
      import(
        './pages/teachers-departments-page/teachers-departments-page.component'
      ).then((component) => component.TeachersDepartmentsPageComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'students',
    loadComponent: () =>
      import('./pages/students-page/students-page.component').then(
        (component) => component.StudentsPageComponent,
      ),
    canActivate: [canActivateAuthAdmin],
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
    path: 'admin-info',
    loadComponent: () =>
      import('./pages/admin-info-page/admin-info-page.component').then(
        (component) => component.AdminInfoPageComponent,
      ),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./pages/user-page/user-page.component').then(
        (mod) => mod.UserPageComponent,
      ),
    canActivate: [CanActivateUserPage],
  },
  {
    path: 'teacher-assignments',
    loadComponent: () =>
      import(
        './pages/assignments-page/teachers-assignment-page/teachers-assignment-page.component'
      ).then((component) => component.TeachersAssignmentPageComponent),
    canActivate: [CanActivateAuthTeacher],
  },
  {
    path: 'administration',
    loadComponent: () =>
      import(
        './pages/user-page/admin-user-short-info/admin-user-short-info.component'
      ).then((component) => component.AdminUserShortInfoComponent),
    canActivate: [canActivateAuthAdmin],
  },
  {
    path: 'current-control-week',
    loadComponent: () =>
      import(
        './pages/current-control-week-page/current-control-week-page.component'
      ).then((component) => component.CurrentControlWeekPageComponent),
    canActivate: [CanActivateUserPage],
  },
];
