export const state = () => ({
    tours: [],
    tourCount: 0
  })
  
  export const getters = {
    getTours(state) {
      return state.tours
    },
    getTourCount(state) {
      return state.tourCount
    }
  }
  
  export const mutations = {
    addTour(state, tour) {
      state.tours.push(tour)
    },
    setTours(state, data)
    {
      state.tours = data.items
      state.tourCount = data.count
    },
    editTour(state, data)
    {
      state.tours.splice(data.idx, 1, data.tour)
    },
    deleteTour(state, data) {
      state.tours.splice(data.idx, 1)
    }
  }
  
  export const actions = {
    getTours({commit}, options) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = options
        this.$axios.$get(`/tours/get?pageNumber=${page}&pageSize=${itemsPerPage}`).then(data => {
          commit('setTours', data)
          resolve(data)
        }).catch(error => reject(error))
      })
    },
    addTour({commit}, tour) {
        return new Promise((resolve, reject) => {
            const newTour = {
              name: tour.name,
              destinations: tour.destinations.map(d => d.destinationID),
            }
            this.$axios.$post('/tours/add', newTour)
            .then(response => {
                tour.tourID = response
                commit('addTour', tour)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editTour({commit}, data) {
      return new Promise((resolve, reject) => {
        const editedTour = {
          name: data.tour.name,
          destinations: data.tour.destinations.map(d => d.destinationID)
        }
        this.$axios.$put(`/tours/edit/${data.tour.tourID}`, editedTour)
        .then(response => {
            commit('editTour', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    },
    deleteTour({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$delete(`/tours/delete/${data.tour.tourID}`)
        .then(response => {
            commit('deleteTour', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }