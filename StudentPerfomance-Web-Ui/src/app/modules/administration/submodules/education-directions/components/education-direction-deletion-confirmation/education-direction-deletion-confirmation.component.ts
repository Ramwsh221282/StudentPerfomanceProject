import {
  AfterViewInit,
  Component,
  EventEmitter,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ActionConfirmationModalComponent } from '../../../../../../shared/components/action-confirmation-modal/action-confirmation-modal.component';

@Component({
  selector: 'app-education-direction-deletion-confirmation',
  templateUrl: './education-direction-deletion-confirmation.component.html',
  styleUrl: './education-direction-deletion-confirmation.component.scss',
  providers: [BsModalService],
})
export class EducationDirectionDeletionConfirmationComponent
  extends ActionConfirmationModalComponent
  implements AfterViewInit
{
  @ViewChild('template') template!: TemplateRef<any>;
  @Output() actionResult: EventEmitter<boolean> = new EventEmitter<boolean>();

  public constructor(private readonly _modalService: BsModalService) {
    super();
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  public close() {
    this._modalService.hide();
    this.actionResult.emit(this.actionResultValue);
  }
}
