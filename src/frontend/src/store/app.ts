// Utilities
import { defineStore } from 'pinia'

export interface State {
  fundId: number | null;
}

export const useAppStore = defineStore('app', {
  state: (): State => ({
    fundId: null,
  }),
  actions: {
    setFundId(fundId: number | null) {
      this.fundId = fundId
    },
  }
})
