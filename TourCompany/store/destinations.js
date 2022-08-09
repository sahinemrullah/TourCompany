export const state = () => ({
    destinations: [],
    destinationCount: 0
  })
  
  export const getters = {
    getDestinations(state) {
      return state.destinations
    },
    getDestinationCount(state) {
      return state.destinationCount
    }
  }
  
  export const mutations = {
    addDestination(state, destination) {
      state.destinations.push(destination)
    },
    setDestinations(state, data)
    {
      state.destinations = data.items
      state.destinationCount = data.count
    },
    editDestination(state, data)
    {
        state.destinations.splice(data.idx, 1, data.destination)
    },
    removeDestination(state, data)
    {
      state.destinations.splice(data.idx, 1)
    }
  }
  
  export const actions = {
    getDestinations({commit}, options) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = options
        this.$axios.$get(`/destinations/get?pageNumber=${page}&pageSize=${itemsPerPage}`).then(data => {
          commit('setDestinations', data)
          resolve(data)
        }).catch(error => reject(error))
      })
    },
    addDestination({commit}, destination) {
        return new Promise((resolve, reject) => {
            this.$axios.$post('/destinations/add', destination)
            .then(response => {
                destination.destinationID = response
                commit('addDestination', destination)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editDestination({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$put(`/destinations/edit/${data.destination.destinationID}`, data.destination)
        .then(response => {
            commit('editDestination', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    },
    deleteDestination({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$delete(`/destinations/delete/${data.destination.destinationID}`)
        .then(response => {
            commit('removeDestination', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }