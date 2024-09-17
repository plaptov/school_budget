import { BaseApiService } from "./base-api-service";

export class CrudService<T> extends BaseApiService {
  private readonly _baseUrl: string;

  constructor(controllerName: string) {
    super();
    this._baseUrl = "/api/" + controllerName;
  }

  public getAll(): Promise<T[]> {
    return this.getRequest(this._baseUrl + "/all");
  }

  public getById(id: number): Promise<T> {
    return this.getRequest(this._baseUrl + "/" + id);
  }

  public create(item: T): Promise<T> {
    return this.postRequest(this._baseUrl, item);
  }

  public update(item: T): Promise<T> {
    return this.putRequest(this._baseUrl, item);
  }

  public delete(id: number): Promise<void> {
    return this.deleteRequest(this._baseUrl + "/" + id);
  }
}
