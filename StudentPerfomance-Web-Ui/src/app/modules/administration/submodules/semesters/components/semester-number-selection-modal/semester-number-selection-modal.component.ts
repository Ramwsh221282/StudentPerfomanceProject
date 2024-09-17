import {
  AfterViewInit,
  Component,
  EventEmitter,
  inject,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-semester-number-selection-modal',
  templateUrl: './semester-number-selection-modal.component.html',
  styleUrl: './semester-number-selection-modal.component.scss',
  providers: [BsModalService],
})
export class SemesterNumberSelectionModalComponent
  implements OnInit, AfterViewInit
{
  @ViewChild('template') _template!: TemplateRef<any>;
  @Output()
  selectedValue = new EventEmitter<number>();
  @Output() modalDisabled = new EventEmitter<boolean>();
  private readonly _modalService: BsModalService;
  protected numbersList: number[];

  public constructor() {
    this._modalService = inject(BsModalService);
    this._modalService.onHide.subscribe(() => this.closeModal());
  }

  public ngOnInit(): void {
    this.numbersList = [];
    for (let i = 1; i <= 10; i++) {
      this.numbersList.push(i);
    }
  }

  public ngAfterViewInit() {
    this._modalService.show(this._template);
  }

  protected closeModal(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }

  protected selectNumberValue(number: number): void {
    this.selectedValue.emit(number);
    this._modalService.hide();
  }
}
