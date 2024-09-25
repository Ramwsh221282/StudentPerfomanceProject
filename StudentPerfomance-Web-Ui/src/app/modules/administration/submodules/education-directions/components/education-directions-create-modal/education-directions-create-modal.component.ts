import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FacadeService } from '../../services/facade.service';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { EducationDirectionCreationHandler } from './education-directions-create-handler';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { TransactionState } from '../../../../../../shared/models/transaction-state/transtaction-state';

@Component({
  selector: 'app-education-directions-create-modal',
  templateUrl: './education-directions-create-modal.component.html',
  styleUrl: './education-directions-create-modal.component.scss',
  providers: [BsModalService],
})
export class EducationDirectionsCreateModalComponent
  extends EducationDirectionBaseForm
  implements AfterViewInit, OnInit, INotificatable
{
  @ViewChild('template') template!: TemplateRef<any>;
  @ViewChild('success') success!: TemplateRef<any>;
  @ViewChild('failure') failure!: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
  public readonly successModalState: ModalState;
  public readonly failureModalState: ModalState;
  public constructor(
    private readonly _modalService: BsModalService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _facadeService: FacadeService
  ) {
    super();
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
    this._modalService.onHide.subscribe(() => this.close());
  }

  protected override submit(): void {
    const handler = EducationDirectionCreationHandler(
      this._facadeService,
      this._notificationService,
      this
    );
    this._facadeService
      .create(this.createEducationDirectionFromForm())
      .pipe(
        tap((response) => {
          handler.handle(response);
          this._modalService.hide();
          this._modalService.show(this.success);
        }),
        catchError((error: HttpErrorResponse) => {
          this._modalService.hide();
          this._modalService.show(this.failure);
          return handler.handleError(error);
        })
      )
      .subscribe();
    this.initForm();
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  public ngOnInit(): void {
    this.setTitle('Создание направления');
    this.initForm();
  }

  public close(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }
}
