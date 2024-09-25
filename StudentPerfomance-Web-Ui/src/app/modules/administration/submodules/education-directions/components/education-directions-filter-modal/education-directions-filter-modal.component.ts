import {
  AfterViewInit,
  Component,
  EventEmitter,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-directions-filter-modal',
  templateUrl: './education-directions-filter-modal.component.html',
  styleUrl: './education-directions-filter-modal.component.scss',
  providers: [BsModalService],
})
export class EducationDirectionsFilterModalComponent
  extends EducationDirectionBaseForm
  implements AfterViewInit, OnInit
{
  @ViewChild('template') template!: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
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
    this.initForm();
    this.setTitle('Фильтрация');
  }

  protected override submit(): void {
    this._facadeService.filter(this.createEducationDirectionFromForm());
    this.initForm();
    this.close();
  }

  protected close(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }
}
