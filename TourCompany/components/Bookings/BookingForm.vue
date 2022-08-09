<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitle }}</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-stepper v-model="step" vertical>
          <v-stepper-step step="1" :rules="stepValues[0].rules">
            Select tour.
            <small v-if="!stepValues[0].isValid">
              {{ stepValues[0].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="1">
            <tour-table
              :show-select="true"
              :tour="tour"
              class="mb-6"
              @on-select="$emit('update:tour', $event); step = step + 1"
            >
            </tour-table>
            <v-btn text @click="step = step - 1"> Previous </v-btn>
            <v-btn v-if="!!tour" color="primary" @click="step = step + 1"> Next </v-btn>
          </v-stepper-content>
          <v-stepper-step step="2" :rules="stepValues[1].rules">
            Select guide.
            <small v-if="!stepValues[1].isValid">
              {{ stepValues[1].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="2">
            <guide-table
              :show-select="true"
              :guide="guide"
              class="mb-6"
              @on-select="$emit('update:guide', $event); step = step + 1"
            >
            </guide-table>
            <v-btn text @click="step = step - 1"> Previous </v-btn>
            <v-btn v-if="!!guide" color="primary" @click="step = step + 1"> Next </v-btn>
          </v-stepper-content>
          <v-stepper-step step="3" :rules="stepValues[2].rules">
            Select participants.
            <small v-if="!stepValues[2].isValid">
              {{ stepValues[2].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="3">
            <tourist-table
              :show-select="true"
              :single-select="false"
              :selected-tourists="tourists"
              class="mb-6"
              @on-select="$emit('update:tourists', $event)"
            >
            </tourist-table>
            <v-btn text @click="step = step - 1"> Previous </v-btn>
            <v-btn color="primary" @click="step = step + 1"> Next </v-btn>
          </v-stepper-content>

          <v-stepper-step step="4" :rules="stepValues[3].rules">
            Select date.
            <small v-if="!stepValues[3].isValid">
              {{ stepValues[3].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="4">
            <v-container>
              <v-row class="mb-3">
                <v-date-picker
                  :value="date"
                  width="300px"
                  :landscape="true"
                  @input="$emit('update:date', $event)"
                >
                </v-date-picker>
              </v-row>
              <v-row>
                <v-btn class="mx-3" @click="step = step - 1"> Previous </v-btn>
                <v-btn
                  :color="isValid ? 'primary' : 'error'"
                  @click="$emit('on-save', isValid); step = isValid ? 1 : step"
                >
                  Submit
                </v-btn>
              </v-row>
            </v-container>
          </v-stepper-content>
        </v-stepper>
      </v-container>
    </v-card-text>
  </v-card>
</template>
<script>
import TourTable from '../Tours/TourTable.vue'
import GuideTable from '../Guides/GuideTable.vue'
import TouristTable from '../Tourists/TouristTable.vue'

export default {
  components: { TourTable, GuideTable, TouristTable },
  props: {
    formTitle: {
      type: String,
      default: 'Title',
    },
    tour: {
      type: Object,
      default: () => null,
    },
    guide: {
      type: Object,
      default: () => null,
    },
    tourists: {
      type: Array,
      default: () => [],
    },
    date: {
      type: String,
      default: new Date().toISOString().split('T')[0],
    },
  },
  emits: [
    'on-save',
    'on-close',
    'update:tour',
    'update:guide',
    'update:tourists',
    'update:date',
  ],
  data() {
    return {
      step: 1,
      stepValues: [
        {
          isValid: false,
          errorMessage: null,
          rules: [() => !!this.tour || 'You need to select tour.'],
        },
        {
          isValid: false,
          errorMessage: null,
          rules: [() => !!this.guide || 'You need to select guide.'],
        },
        {
          isValid: false,
          errorMessage: null,
          rules: [
            () =>
              (this.tourists.length > 0) || 'You need to select at least 1 tourist.',
          ],
        },
        {
          isValid: false,
          errorMessage: null,
          rules: [ () => true ],
        },
      ],
    }
  },
  computed: {
    isValid() {
      this.stepValues.forEach((v) => {
        v.isValid = v.rules.every((validate) => {
          const validation = validate()
          if (validation === true) return true
          v.errorMessage = validation
          return false
        })
      })
      return this.stepValues.every(v => v.isValid)
    }
  },
  methods: {
    
  },
}
</script>