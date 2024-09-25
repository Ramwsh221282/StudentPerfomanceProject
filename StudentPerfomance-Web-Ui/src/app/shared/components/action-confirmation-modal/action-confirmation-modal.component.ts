import {
  AfterViewInit,
  Component,
  EventEmitter,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-action-confirmation-modal',
  standalone: true,
  imports: [],
  templateUrl: './action-confirmation-modal.component.html',
  styleUrl: './action-confirmation-modal.component.scss',
  providers: [BsModalService],
})
export class ActionConfirmationModalComponent implements AfterViewInit {
  @ViewChild('template') template!: TemplateRef<any>;
  @Output() actionResult: EventEmitter<boolean> = new EventEmitter<boolean>();
  private _actionResult: boolean = false;

  public constructor(private readonly _modalService: BsModalService) {
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  protected confirm() {
    this._actionResult = true;
    this.close();
  }

  protected decline() {
    this._actionResult = false;
    this.close();
  }

  public close() {
    this._modalService.hide();
    this.actionResult.emit(this._actionResult);
  }
}
