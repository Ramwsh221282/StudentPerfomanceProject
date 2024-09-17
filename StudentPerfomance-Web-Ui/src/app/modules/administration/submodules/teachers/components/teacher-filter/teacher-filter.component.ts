import { Component, OnInit } from '@angular/core';
import { BaseTeacherForm } from '../../models/base-teacher-form';
import { FacadeTeacherService } from '../../services/facade-teacher.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-teacher-filter',
  templateUrl: './teacher-filter.component.html',
  styleUrl: './teacher-filter.component.scss',
})
export class TeacherFilterComponent extends BaseTeacherForm implements OnInit {
  public constructor(
    private readonly _facadeService: FacadeTeacherService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Фильтр преподавателей');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected filter(): void {
    this._facadeService.filter(
      this.createTeacherFromForm(this._facadeService.currentDepartment)
    );
    this.initForm();
    this.submit();
  }

  protected cancel(): void {
    this._facadeService.fetch();
    this.submit();
  }
}
