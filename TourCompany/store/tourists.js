export const state = () => ({
    tourists: [],
    touristCount: 0
  })
  
  export const getters = {
    getTourists(state) {
      return state.tourists
    },
    getTouristCount(state) {
      return state.touristCount
    }
  }
  
  export const mutations = {
    addTourist(state, tourist) {
      state.tourists.push(tourist)
    },
    setTourists(state, data)
    {
      state.tourists = data.items
      state.touristCount = data.count
    },
    setTourist(state, data) {
      state.tourists.splice(data.idx, 1, data.tourist);
    }
  }
  
  export const actions = {
    getTourists({commit}, options) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = options
        this.$axios.$get(`/tourists/get?pageNumber=${page}&pageSize=${itemsPerPage}`).then(data => {
          commit('setTourists', data)
          resolve(data)
        }).catch(error => reject(error))
      })
    },
    postTourist({commit}, tourist) {
        return new Promise((resolve, reject) => {
          const addedTourist = {
            touristID: tourist.touristID,
            name: tourist.name,
            surname: tourist.surname,
            birthDate: tourist.birthDate,
            genderID: tourist.genderID,
            countryID: tourist.countryID,
            nationalityID: tourist.nationalityID,
          }
            this.$axios.$post('/tourists/add', addedTourist)
            .then(response => {
                tourist.touristID = response;
                commit('addTourist', tourist)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editTourist({commit}, data) {
        return new Promise((resolve, reject) => {
        const editedTourist = {
          touristID: data.tourist.touristID,
          name: data.tourist.name,
          surname: data.tourist.surname,
          birthDate: data.tourist.birthDate,
          genderID: data.tourist.genderID,
          countryID: data.tourist.countryID,
          nationalityID: data.tourist.nationalityID,
        }
        this.$axios.$put(`/tourists/edit/${data.tourist.touristID}`, editedTourist)
        .then(response => {
            commit('setTourist', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }