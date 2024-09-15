import {
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
  selector: 'app-semesters-select-number-modal-form',
  standalone: true,
  imports: [],
  templateUrl: './semesters-select-number-modal-form.component.html',
  styleUrl: './semesters-select-number-modal-form.component.scss',
  providers: [BsModalService],
})
export class SemestersSelectNumberModalFormComponent implements OnInit {
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

  private ngAfterViewInit() {
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
