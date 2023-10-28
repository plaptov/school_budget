export class BaseApiService {
  protected getRequest<T>(url: string): Promise<T> {
    return this.fetch(url);
  }

  protected postRequest<T>(url: string, body: any): Promise<T> {
    return this.fetch(url, { method: 'POST', body });
  }

  protected putRequest<T>(url: string, body: any): Promise<T> {
    return this.fetch(url, { method: 'PUT', body });
  }

  protected deleteRequest<T>(url: string): Promise<T> {
    return this.fetch(url, { method: 'DELETE' });
  }

  protected fetch<T>(url: string, init?: RequestInit): Promise<T> {
    return fetch(url, init).then((response) => {
      if (!response.ok) {
        throw new Error(response.statusText);
      }
      return response.json() as Promise<T>;
    });
  }
}
