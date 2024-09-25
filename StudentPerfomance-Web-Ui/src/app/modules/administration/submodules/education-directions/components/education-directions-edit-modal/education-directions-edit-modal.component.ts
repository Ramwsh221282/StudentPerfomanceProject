import {
  AfterViewInit,
  Component,
  EventEmitter,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FacadeService } from '../../services/facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { CreateEducationDirectionEditHandler } from './education-direction-edit-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-directions-edit-modal',
  templateUrl: './education-directions-edit-modal.component.html',
  styleUrl: './education-directions-edit-modal.component.scss',
  providers: [BsModalService],
})
export class EducationDirectionsEditModalComponent
  extends EducationDirectionBaseForm
  implements OnInit, AfterViewInit, INotificatable
{
  @ViewChild('template') template!: TemplateRef<any>;
  @ViewChild('success') successTemplate!: TemplateRef<any>;
  @ViewChild('failure') failureTemplate!: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
  public readonly successModalState: ModalState;
  public readonly failureModalState: ModalState;
  public constructor(
    protected readonly facadeService: FacadeService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _modalService: BsModalService
  ) {
    super();
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
    this._modalService.onHide.subscribe(() => this.close());
  }
  protected override submit(): void {
    const oldDirection = this.facadeService.copy;
    const handler = CreateEducationDirectionEditHandler(
      oldDirection,
      this.facadeService,
      this._notificationService,
      this
    );
    this.facadeService
      .update(oldDirection, this.createEducationDirectionFromForm())
      .pipe(
        tap((response) => {
          handler.handle(response);
          this._modalService.hide();
          this._modalService.show(this.successTemplate);
        }),
        catchError((error: HttpErrorResponse) => {
          this._modalService.hide();
          this._modalService.show(this.successTemplate);
          return handler.handleError(error);
        })
      )
      .subscribe();
    this.initForm();
  }
  ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }
  ngOnInit(): void {
    this.initForm();
    this.setTitle('Редактирование');
  }

  protected close(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }
}
