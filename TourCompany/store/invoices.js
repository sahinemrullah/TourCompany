export const state = () => ({
    invoices: [],
    invoiceCount: 0,
    unpaidBookings: [],
    invoice: null
  })
  
  export const getters = {
    getInvoices(state) {
      return state.invoices
    },
    getInvoiceCount(state) {
      return state.invoiceCount
    },
    getUnpaidBookings(state) {
        return state.unpaidBookings
    },
    getInvoice(state) {
      return state.invoice
    }
  }
  
  export const mutations = {
    addInvoice(state, invoice) {
      state.invoices.push(invoice)
    },
    setInvoices(state, data)
    {
      state.invoices = data.items
      state.invoiceCount = data.count
    },
    editInvoice(state, data)
    {
        state.invoices.splice(data.idx, 1, data.invoice)
    },
    removeInvoice(state, data)
    {
      state.invoices.splice(data.idx, 1)
    },
    resetUnpaidBookings(state) {
        state.unpaidBookings = []
    },
    setUnpaidBookings(state, data) {
        state.unpaidBookings = data
    },
    setInvoice(state, invoice) {
      state.invoice = invoice
    }
  }
  
  export const actions = {
    getInvoices({commit}, data) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = data.options
        this.$axios.$get(`/invoices/get?pageNumber=${page}&pageSize=${itemsPerPage}&filter=${data.search}`).then(response => {
          commit('setInvoices', response)
          resolve(response)
        }).catch(error => reject(error))
      })
    },
    addInvoice({commit}, invoice) {
      const addedInvoice = {
        touristID: invoice.tourist.touristID,
        bookings: invoice.bookings.map(b => b.booking.bookingID)
      }
        return new Promise((resolve, reject) => {
            this.$axios.$post('/invoices/add', addedInvoice)
            .then(response => {
                invoice.invoiceID = response
                commit('addInvoice', invoice)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editInvoice({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$put(`/invoices/edit/${data.invoice.invoiceID}`, data.invoice)
        .then(response => {
            commit('editInvoice', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    },
    deleteInvoice({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$delete(`/invoices/delete/${data.invoice.invoiceID}`)
        .then(response => {
            commit('removeInvoice', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    },
    getUnpaidBookingsByTourist({commit}, touristID) {
        return new Promise((resolve, reject) => {
            this.$axios.$get(`/invoices/getUnpaidBookingsByTourist/${touristID}`)
            .then(response => {
                commit('setUnpaidBookings', response)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    getInvoiceDetails({commit}, invoiceNo) {
      return new Promise((resolve, reject) => {
        this.$axios.$get(`/invoices/get/${invoiceNo}`)
        .then(response => {
            commit('setInvoice', response)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }