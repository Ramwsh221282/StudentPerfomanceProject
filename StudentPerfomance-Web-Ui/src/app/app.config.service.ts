import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppConfigService {
  private _configFile: any;
  private _baseApiUri: string = 'http://localhost:5002';

  public constructor(private http: HttpClient) {}

  public async loadAppConfig(): Promise<void> {
    if (!this._configFile) {
      const response = await firstValueFrom(
        this.http.get<any>('/sperfomanceconfig/config.json').pipe(),
      );
      this._configFile = response;
    }
  }

  public initializeBaseApiUri(): void {
    if (this._configFile) {
      this._baseApiUri = this._configFile['BASE_API_URI'];
    } else this._baseApiUri = 'http://localhost:5002';
  }

  public get baseApiUri(): string {
    return this._baseApiUri;
  }
}
