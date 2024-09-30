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
import { EducationPlanBaseForm } from '../../models/education-plan-base-form';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { FacadeService } from '../../services/facade.service';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { BsModalService } from 'ngx-bootstrap/modal';
import { SearchDirectionsService } from '../../../education-directions/services/search-directions.service';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { EducationPlanCreationHandler } from './education-plan-create-handler';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-plans-create-modal',
  templateUrl: './education-plans-create-modal.component.html',
  styleUrl: './education-plans-create-modal.component.scss',
  providers: [BsModalService, SearchDirectionsService],
})
export class EducationPlansCreateModalComponent
  extends EducationPlanBaseForm
  implements AfterViewInit, OnInit, INotificatable
{
  @Input({ required: true }) formTitle: string;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
  @ViewChild('template') template!: TemplateRef<any>;
  @ViewChild('success') success!: TemplateRef<any>;
  @ViewChild('failure') failure!: TemplateRef<any>;
  public readonly successModalState: ModalState;
  public readonly failureModalState: ModalState;
  protected _directions: EducationDirection[];
  protected selectedDirection: EducationDirection;
  protected _codeSearchInput: string;
  protected _nameSearchInput: string;
  public constructor(
    private readonly facadeService: FacadeService,
    private readonly _modalService: BsModalService,
    private readonly _searchService: SearchDirectionsService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super();
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
    this._directions = [];
    this.selectedDirection = {} as EducationDirection;
    this._codeSearchInput = '';
    this._nameSearchInput = '';
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngOnInit(): void {
    this.defaultSearch();
    this.title = this.formTitle;
    this.initForm();
  }
  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  protected defaultSearch(): void {
    this._searchService
      .getAll()
      .subscribe((response) => (this._directions = response));
  }

  protected searchByNameClick(): void {
    const name = this._nameSearchInput == null ? '' : this._nameSearchInput;
    const direction = { code: '', name: name, type: '' } as EducationDirection;
    const factory =
      this._searchService.createSearchRequestParamFactory(direction);
    this._searchService
      .filterPagedByName(factory)
      .subscribe((response) => (this._directions = response));
    this._nameSearchInput = '';
  }

  protected searchByCodeClick(): void {
    const code = this._codeSearchInput == null ? '' : this._codeSearchInput;
    const direction = { code: code, name: '', type: '' } as EducationDirection;
    const factory =
      this._searchService.createSearchRequestParamFactory(direction);
    this._searchService
      .filterPagedByCode(factory)
      .subscribe((response) => (this._directions = response));
    this._codeSearchInput = '';
  }

  protected override submit(): void {
    const handler = EducationPlanCreationHandler(
      this.facadeService,
      this._notificationService,
      this
    );
    this.facadeService
      .create(this.createEducationPlanFromForm())
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

  protected close(): void {
    this.modalDisabled.emit(false);
    this._modalService.hide();
  }

  protected selectDirection(direction: EducationDirection): void {
    this.selectedDirection = { ...direction };
  }
}
