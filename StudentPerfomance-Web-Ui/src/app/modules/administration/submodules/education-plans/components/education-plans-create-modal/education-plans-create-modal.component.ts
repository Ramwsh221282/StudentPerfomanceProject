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
  public constructor(
    private readonly facadeService: FacadeService,
    private readonly _modalService: BsModalService,
    private readonly _searchService: SearchDirectionsService
  ) {
    super();
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
    this._directions = [];
    this.selectedDirection = {} as EducationDirection;
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngOnInit(): void {
    this._searchService
      .getAll()
      .subscribe((response) => (this._directions = response));
    this.title = this.formTitle;
    this.initForm();
  }
  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }
  protected override submit(): void {
    throw new Error('Method not implemented.');
  }

  protected close(): void {
    this.modalDisabled.emit(false);
    this._modalService.hide();
  }

  protected selectDirection(direction: EducationDirection): void {
    this.selectedDirection = { ...direction };
  }
}
