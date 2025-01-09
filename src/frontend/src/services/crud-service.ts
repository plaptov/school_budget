import { BaseApiService, paramsType } from "./base-api-service";

export class CrudService<T> extends BaseApiService {
  private readonly _baseUrl: string;

  constructor(controllerName: string) {
    super();
    this._baseUrl = "/api/" + controllerName;
  }

  private makeUrl(end: unknown): string {
    return `${this._baseUrl}/${end}`;
  }

  public getAll(): Promise<T[]> {
    return this.getRequest(this.makeUrl("all"));
  }

  public getById(id: number): Promise<T> {
    return this.getRequest(this.makeUrl(id));
  }

  public create(item: T): Promise<T> {
    return this.postRequest(this._baseUrl, item);
  }

  public update(item: T): Promise<T> {
    return this.putRequest(this._baseUrl, item);
  }

  public delete(id: number): Promise<void> {
    return this.deleteRequest(this.makeUrl(id));
  }

  protected get<TT = T>(url: string, queryParams?: paramsType): Promise<TT> {
    return this.getRequest(this.makeUrl(url), queryParams);
  }

  protected post<TT>(url: string, body: unknown): Promise<TT> {
    return this.postRequest(this.makeUrl(url), body);
  }
}
