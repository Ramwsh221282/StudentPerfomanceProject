import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-administration-menu',
    templateUrl: './administration-menu.component.html',
    styleUrl: './administration-menu.component.scss',
    standalone: false
})
export class AdministrationMenuComponent {
  @Input({ required: true }) public title!: string;
  @Input({ required: true }) public routerLinkPath!: string[];
  @Input({ required: true }) public descriptions: string[];

  public constructor(private readonly router: Router) {}

  public onButtonClick(): void {
    this.router.navigate(this.routerLinkPath); // ignore
  }
}
