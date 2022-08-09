export const state = () => ({
    guides: [],
    guideCount: 0
  })
  
  export const getters = {
    getGuides(state) {
      return state.guides
    },
    getGuideCount(state) {
      return state.guideCount
    }
  }
  
  export const mutations = {
    addGuide(state, guide) {
      state.guides.push(guide)
    },
    setGuides(state, data)
    {
      state.guides = data.items
      state.guideCount = data.count
    },
    setGuide(state, data) {
      state.guides.splice(data.idx, 1, data.guide);
    },
    deleteGuide(state, data) {
      state.guides.splice(data.idx, 1)
    }
  }
  
  export const actions = {
    getGuides({commit}, options) {
      return new Promise((resolve, reject) => {
        const { page, itemsPerPage } = options
        this.$axios.$get(`/guides/get?pageNumber=${page}&pageSize=${itemsPerPage}`).then(data => {
          commit('setGuides', data)
          resolve(data)
        }).catch(error => reject(error))
      })
    },
    postGuide({commit}, guide) {
        return new Promise((resolve, reject) => {
            const addedGuide = {
              name: guide.name,
              surname: guide.surname,
              telephoneNumber: guide.telephoneNumber,
              genderID: guide.gender.genderID,
              languages: guide.languages
            }
            this.$axios.$post('/guides/add', addedGuide)
            .then(response => {
                guide.guideID = response;
                commit('addGuide', guide)
                resolve(response)
            })
            .catch(error => reject(error));
          })
    },
    editGuide({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$put(`/guides/edit/${data.guide.guideID}`, data.guide)
        .then(response => {
            commit('setGuide', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    },
    deleteGuide({commit}, data) {
      return new Promise((resolve, reject) => {
        this.$axios.$delete(`/guides/delete/${data.guide.guideID}`)
        .then(response => {
            commit('deleteGuide', data)
            resolve(response)
        })
        .catch(error => reject(error));
      })
    }
  }