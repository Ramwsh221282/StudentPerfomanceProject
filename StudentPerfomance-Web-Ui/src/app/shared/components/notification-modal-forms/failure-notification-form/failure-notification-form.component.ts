import {
  AfterViewInit,
  Component,
  EventEmitter,
  inject,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-failure-notification-form',
  standalone: true,
  imports: [],
  templateUrl: './failure-notification-form.component.html',
  styleUrl: './failure-notification-form.component.scss',
  providers: [BsModalService],
})
export class FailureNotificationFormComponent implements AfterViewInit {
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter();
  @ViewChild('template') _template!: TemplateRef<any>;
  private readonly _modalService = inject(BsModalService);
  protected readonly notificationService = inject(
    UserOperationNotificationService
  );

  public constructor() {
    this._modalService.onHide.subscribe(() => this.close());
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this._template);
  }

  protected close(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }
}
