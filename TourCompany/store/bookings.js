export const state = () => ({
    bookings: [],
    bookingCount: 0
  })
  
  export const getters = {
    getBookings(state) {
      return state.bookings
    },
    getBookingCount(state) {
      return state.bookingCount
    }
  }
  
  export const mutations = {
    addBooking(state, booking) {
      booking.date = new Date(Date.parse(booking.date)).toISOString()
      state.bookings.push(booking)
    },
    setBookings(state, data)
    {
      state.bookings = data.items
      state.bookingCount = data.count
    },
    editBooking(state, data)
    {
      data.booking.date = new Date(Date.parse(data.booking.date)).toISOString()
      state.bookings.splice(data.idx, 1, data.booking)
    }
  }
  
  export const actions = {
    getBookings({commit}, options) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = options
        this.$axios.$get(`/bookings/get?pageNumber=${page}&pageSize=${itemsPerPage}`).then(data => {
          commit('setBookings', data)
          resolve(data)
        }).catch(error => reject(error))
      })
    },
    postBooking({commit}, booking) {
        return new Promise((resolve, reject) => {
          const addedBooking = {
            tourID: booking.tour.tourID,
            guideID: booking.guide.guideID,
            tourists: booking.tourists.map(t => t.touristID),
            date: new Date(Date.parse(booking.date)).toISOString()
          }
            this.$axios.$post('/bookings/add', addedBooking)
            .then(response => {
                booking.bookingID = response
                commit('addBooking', booking)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editBooking({commit}, data) {
      const editedBooking = {
        bookingID: data.booking.bookingID,
        tourID: data.booking.tour.tourID,
        guideID: data.booking.guide.guideID,
        tourists: data.booking.tourists.map(t => t.touristID),
        date: new Date(data.booking.date).toISOString()
      }
      return new Promise((resolve, reject) => {
        this.$axios.$put(`/bookings/edit/${data.booking.bookingID}`, editedBooking)
        .then(response => {
            commit('editBooking', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }