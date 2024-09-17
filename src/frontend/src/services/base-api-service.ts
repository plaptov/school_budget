const jsonHeaders = new Headers({ "Content-Type": "application/json" });
type paramsType = string[][] | Record<string, string> | string | URLSearchParams;

export class BaseApiService {
  protected getRequest<T>(url: string, queryParams?: paramsType): Promise<T> {
    if (queryParams) {
      url += `?${new URLSearchParams(queryParams)}`;
    }

    return this.fetch(url);
  }

  protected postRequest<T>(url: string, body: any): Promise<T> {
    return this.fetch(url, { method: 'POST', headers: jsonHeaders, body: JSON.stringify(body) });
  }

  protected putRequest<T>(url: string, body: any): Promise<T> {
    return this.fetch(url, { method: 'PUT', headers: jsonHeaders, body: JSON.stringify(body) });
  }

  protected deleteRequest(url: string): Promise<void> {
    return this.fetchVoid(url, { method: 'DELETE' });
  }

  protected async fetch<T>(url: string, init?: RequestInit): Promise<T> {
    const response = await fetch(url, init);
    if (!response.ok) {
      throw new Error(response.statusText);
    }
    return await response.json() as T;
  }

  protected async fetchVoid(url: string, init?: RequestInit): Promise<void> {
    const response = await fetch(url, init);
    if (!response.ok) {
      throw new Error(response.statusText);
    }
  }
}
