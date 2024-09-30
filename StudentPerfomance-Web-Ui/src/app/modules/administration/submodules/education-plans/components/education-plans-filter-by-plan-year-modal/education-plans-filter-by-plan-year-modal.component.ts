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
import { FacadeService } from '../../services/facade.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';

@Component({
  selector: 'app-education-plans-filter-by-plan-year-modal',
  templateUrl: './education-plans-filter-by-plan-year-modal.component.html',
  styleUrl: './education-plans-filter-by-plan-year-modal.component.scss',
  providers: [BsModalService],
})
export class EducationPlansFilterByPlanYearModalComponent
  extends EducationPlanBaseForm
  implements AfterViewInit, OnInit
{
  @Input({ required: true }) public formTitle: string;
  @ViewChild('template') template!: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter();

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _modalService: BsModalService
  ) {
    super();
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  public ngOnInit(): void {
    this.title = this.formTitle;
    this.initForm();
  }
  protected override submit(): void {
    const plan = this.createEducationPlanFromForm();
    plan.direction = {} as EducationDirection;
    plan.direction.code = '';
    plan.direction.name = '';
    plan.direction.type = '';
    this._facadeService.filterByYear(plan);
    this.initForm();
    this.close();
  }

  protected close(): void {
    this.modalDisabled.emit(false);
    this._modalService.hide();
  }
}
