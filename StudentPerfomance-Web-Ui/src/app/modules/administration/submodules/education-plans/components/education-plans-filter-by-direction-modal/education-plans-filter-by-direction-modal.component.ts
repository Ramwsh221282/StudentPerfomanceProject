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

@Component({
  selector: 'app-education-plans-filter-by-direction-modal',
  templateUrl: './education-plans-filter-by-direction-modal.component.html',
  styleUrl: './education-plans-filter-by-direction-modal.component.scss',
  providers: [BsModalService],
})
export class EducationPlansFilterByDirectionModalComponent
  extends EducationPlanBaseForm
  implements OnInit, AfterViewInit
{
  @Input({ required: true }) public formTitle: string;
  @ViewChild('template') template!: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _modalService: BsModalService
  ) {
    super();
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngOnInit(): void {
    this.title = this.formTitle;
    this.initForm();
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  protected override submit(): void {
    const plan = this.createEducationPlanFromForm();
    plan.year = 0;
    plan.direction.code = '';
    plan.direction.type = '';
    this._facadeService.filterByDirection(plan);
    this.initForm();
    this.close();
  }

  protected close(): void {
    this.modalDisabled.emit(false);
    this._modalService.hide();
  }
}
