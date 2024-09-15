import {
  AfterViewInit,
  Component,
  EventEmitter,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { StudentGroupSearchService } from '../../../dbmanager-studentgroups-view/services/student-group-search.service';
import { StudentGroup } from '../../../dbmanager-studentgroups-view/services/studentsGroup.interface';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-semesters-search-group-modal-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './semesters-search-group-modal-form.component.html',
  styleUrl: './semesters-search-group-modal-form.component.scss',
  providers: [BsModalService, StudentGroupSearchService],
})
export class SemestersSearchGroupModalFormComponent
  implements OnInit, AfterViewInit
{
  @Output() selectedGroup: EventEmitter<string>;
  @Output() modalDisabled: EventEmitter<boolean>;
  @ViewChild('template') _template!: TemplateRef<any>;
  protected inputText: string;
  protected searchedResults: StudentGroup[];
  protected isErrorVisible: boolean;
  protected errorMessage: string;

  public constructor(
    private readonly _modalService: BsModalService,
    private readonly _searchService: StudentGroupSearchService
  ) {
    this.selectedGroup = new EventEmitter<string>();
    this.modalDisabled = new EventEmitter<boolean>();
    this._modalService.onHide.subscribe(() => this.closeModal());
  }

  public ngOnInit(): void {
    this.inputText = '';
    this.searchedResults = [];
    this.isErrorVisible = true;
    this.errorMessage = '';
  }

  public searchClick(): void {
    const factory = this._searchService.createRequestParamsFactory({
      groupName: this.inputText,
    } as StudentGroup);
    this._searchService
      .searchByName(factory)
      .pipe(
        tap((response) => {
          this.searchedResults = response;
          this.searchedResults.length == 0
            ? this.showNotification('Ничего не найдено')
            : this.showNotification('Посмотрите результаты');
        }),
        catchError((error: HttpErrorResponse) => (this.searchedResults = []))
      )
      .subscribe();
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this._template);
  }

  protected closeModal(): void {
    this.searchedResults = [];
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }

  protected selectStudentGroup(selectedGroupName: string): void {
    this.selectedGroup.emit(selectedGroupName);
    this._modalService.hide();
  }

  private showNotification(errorMessage: string): void {
    this.isErrorVisible = true;
    this.errorMessage = errorMessage;
  }
}
